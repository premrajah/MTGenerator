(function ($)
{

    $.fn.mercuryZone = function (options)
    {

        // Default Options.
        var settings = $.extend({
            backgroundColor: "transparent",
            refreshTime: 1000,
            transitionDuration: 1000,
            transitionType: "FadeOutFadeIn"
        }, options);

        let optionValidationErrors = false;
        if (!settings.zoneName)
        {
            jQuery.error("mercuryZone:  No zoneName option is provided.");
            optionValidationErrors = true;
        }
        if (!settings.pageRelativeURL)
        {
            jQuery.error("mercuryZone:  No pageRelativeURL option is provided.");
            optionValidationErrors = true;
        }

        if (optionValidationErrors == true)
        {
            return this;
        }



        // Greenify the collection based on the settings variable.
        let ZoneFrames = `<div id="` + settings.zoneName + `" style="margin:0; padding:0; width:100%; height:100%; background-color:` + settings.backgroundColor + `;">
                <div class="buffer0" style="position:absolute; width:100%; height:100%; overflow:hidden; opacity:0; z-index:1">
                    <iframe id="buffer0Content" src="" width="100%" height="100%" scrolling="no" style="border:0;"></iframe>
                </div>
                <div class="buffer1" style="position:absolute; width:100%; height:100%; overflow:hidden; opacity:0; z-index:0">
                    <iframe id="buffer1Content" src="" width="100%" height="100%" scrolling="no" style="border:0;"></iframe>
                </div>
                <div style="cursor:none; position:fixed; height:100%; width:100%; z-index:999;"></div>
            </div>`

        this.html(ZoneFrames);

        startZone(this);

        let guid, buffer, CurrentType, PreviousType, countdownInterval, countdown;
        let mercuryUpdatedTime = 0;
        let UI_buffer0 = this.find(".buffer0");
        let UI_buffer1 = this.find(".buffer1");
        let UI_content0 = this.find("#buffer0Content");
        let UI_content1 = this.find("#buffer1Content");

        return this;







        function startZone(ZoneController)
        {

            let deviceID = fetchDeviceID();
            if (deviceID == null)
            {
                jQuery.error("mercuryZone:  ID parameter missing from URL");
                return;
            }

            setInterval(function ()
            {
                fetchZoneData(deviceID, ZoneController);
            }, settings.refreshTime);
        }

        function fetchDeviceID()
        {
            function getParameterByName(name)
            {
                var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
                return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
            }

            //get device ID
            return getParameterByName("ID");
        }

        function fetchZoneData(deviceID, ZoneController)
        {
            try
            {
                let dataURL = `/Data/${deviceID}_${settings.zoneName}.json?_=${new Date().getTime()}`;
                $.getJSON(dataURL, function (json)
                {
                    processZoneData(deviceID, json, ZoneController);
                });
            }
            catch (e) { }
        }

        function processZoneData(deviceID, json, ZoneController)
        {


            if (json.MercuryPlaylists == undefined)
            {
                return;
            }

            //check to see if the guid and UpdatedTime has changed and this is new data from the server
            if (guid != json.MercuryPlaylists.NextItem.Guid && mercuryUpdatedTime < json.MercuryPlaylists.NextItem.UpdatedTime)
            {

                // if (json.MercuryPlaylists.Control.Invoked == false && countdown > 0.0 && CurrentType == "VT")
                // {
                //     //if the timer has not completed and the user has not invoked the change then return;
                //     return;
                // }

                guid = json.MercuryPlaylists.NextItem.Guid;
                mercuryUpdatedTime = json.MercuryPlaylists.NextItem.UpdatedTime;

                //lookup type to use
                var type = json.MercuryPlaylists.NextItem.Type;
                PreviousType = CurrentType;
                CurrentType = type;

                var template;
                try
                {
                    template = json.MercuryPlaylists[type].Template;
                }
                catch (e)
                { }

                if (template == undefined)
                {
                    template = json.MercuryPlaylists.Project[type].Template;
                }

                //check to see if this template should be listening to this data
                if (!template.includes(settings.pageRelativeURL))
                {
                    return;
                }

                //set elements
                if(settings.beforeTransitionCallback)
                {
                    if(settings.beforeTransitionCallback(CurrentType, PreviousType) == false)
                    {
                        return;
                    }
                }
                MixBuffers(deviceID, `./${settings.zoneName}/${type}.html`);

                //set countdown var
                var cd

                try
                {
                    cd = json.MercuryPlaylists[type].ItemDuration;
                }
                catch (e)
                { }

                if (cd == undefined)
                {
                    cd = json.MercuryPlaylists.Project[type].ItemDuration;
                }

                countdown = cd;

            }


            function MixBuffers(deviceID, url)
            {
                //load the next template and mix in and mix out the current buffer
                if (buffer == undefined)
                {
                    buffer = 999;
                }

                switch (buffer)
                {
                    case 0:
                        UI_content1.attr("src", `${url}?pl=${deviceID}_${settings.zoneName}&_=${new Date().getTime()}`);                        

                        switch (settings.transitionType)
                        {
                            case "FadeOutFadeIn":

                                UI_buffer0.fadeTo(settings.transitionDuration / 2, 0, function ()
                                {
                                    //RemoveOldContentFromBuffer(0);
                                    UI_buffer1.fadeTo(settings.transitionDuration / 2, 1, function ()
                                    {
                                        UI_content0.attr("src", "about:blank");
                                    });
                                });
                                break;
                            case "Crossfade":

                                UI_buffer0.fadeTo(settings.transitionDuration, 0);
                                UI_buffer1.fadeTo(settings.transitionDuration, 1, function ()
                                {
                                    UI_content0.attr("src", "about:blank");
                                });
                                break;
                        }

                        //stop any running countdowns
                        clearInterval(countdownInterval);
                        //start the countdown
                        countdownInterval = setInterval(countdownTimer, 1000);

                        buffer = 1;
                        break;
                    case 1:
                        UI_content0.attr("src", `${url}?pl=${deviceID}_${settings.zoneName}&_=${new Date().getTime()}`);                        
                       
                        switch (settings.transitionType)
                        {
                            case "FadeOutFadeIn":

                                UI_buffer1.fadeTo(settings.transitionDuration / 2, 0, function ()
                                {
                                    //RemoveOldContentFromBuffer(0);
                                    UI_buffer0.fadeTo(settings.transitionDuration / 2, 1, function ()
                                    {
                                        UI_content1.attr("src", "about:blank");
                                    });
                                });
                                break;
                            case "Crossfade":

                                UI_buffer1.fadeTo(settings.transitionDuration, 0);
                                UI_buffer0.fadeTo(settings.transitionDuration, 1, function ()
                                {
                                    UI_content1.attr("src", "about:blank");
                                });
                                break;
                        }                           

                        //stop any running countdowns
                        clearInterval(countdownInterval);
                        //start the countdown
                        countdownInterval = setInterval(countdownTimer, 1000);

                        buffer = 0;
                        break;
                    case 999:
                        UI_content0.attr("src", `${url}?pl=${deviceID}_${settings.zoneName}&_=${new Date().getTime()}`);
                        UI_buffer0.fadeTo(500, 1);

                        //stop any running countdowns
                        clearInterval(countdownInterval);
                        //start the countdown
                        countdownInterval = setInterval(countdownTimer, 1000);

                        buffer = 0;
                        break;
                }
            }


            function countdownTimer()
            {
                if (countdown > 0.0)
                {
                    //send countdown commands
                    countdown = countdown - 1.0;
                }
                if (countdown <= 0.0)
                {
                    //stop the countdown
                    clearInterval(countdownInterval);
                }
            }

            function RemoveOldContentFromBuffer(buf)   
            {
                //unload the content in the old buffer
                switch (buf)
                {
                    case 0:
                        UI_content0.attr("src", "about:blank");
                        break;
                    case 1:
                        UI_content1.attr("src", "about:blank");
                        break;
                }
            }
        }



    };

}(jQuery));