$(document).ready(function () {
    function getParameterByName(name) {
        var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    }

    var lastGuid;
    ReadPlaylistData();
    //setInterval(ReadPlaylistData, 1000);

    function ReadPlaylistData() {
        //get json from url query
        playlist = getParameterByName("pl");

        try {
            //get json data from the server
            $.getJSON("/Data/" + playlist + ".json?_=" + new Date().getTime(), function (data) {
                if (data.MercuryPlaylists.NextItem.Guid != lastGuid) {
                    lastGuid = data.MercuryPlaylists.NextItem.Guid;

                    //get the current item type
                    var type = data.MercuryPlaylists.NextItem.Type;

                    var mercuryEvent = jQuery.Event("itemData");

                    try {
                        mercuryEvent.itemData = data.MercuryPlaylists[type];

                        if (mercuryEvent.itemData != undefined) {
                            $(window).trigger(mercuryEvent);
                        }

                    }
                    catch (e) { }

                    if (mercuryEvent.itemData == undefined) {
                        mercuryEvent.itemData = data.MercuryPlaylists.Project[type];
                        $(window).trigger(mercuryEvent);
                    }
                }

            });
        }
        catch (ex) { }
    }
});