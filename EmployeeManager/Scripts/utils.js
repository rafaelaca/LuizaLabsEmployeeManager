function ResizeIframeHeightOnLoad(p_IframeSelector, p_MinimumHeight) {
    var l_IFrame;

    if (p_IframeSelector != "" && p_IframeSelector != undefined && p_IframeSelector != null) {
        l_IFrame = $(p_IframeSelector);
    }
    else {
        l_IFrame = $("iframe");
    }

    l_IFrame.load(function () {
        $(this).fadeIn();
        var l_height = 0;

        try {
            l_height = $(this.contentWindow.document).outerHeight();
        } catch (e) { }

        if ($(this).data("minheight") != "" && $(this).data("minheight") != null && $(this).data("minheight") != undefined) {
            l_height = $(this).data("minheight");
        }

        if (p_MinimumHeight != 0 && p_MinimumHeight != "" && p_MinimumHeight != null) {
            if (l_height < p_MinimumHeight)
                l_height = p_MinimumHeight;
        }

        $(this).height(l_height);
    });
}

function ResizeIframeHeight(p_IframeSelector, p_MinimumHeight)
{
    var l_IFrame;

    if (p_IframeSelector != "" && p_IframeSelector != undefined && p_IframeSelector != null) {
        l_IFrame = $(p_IframeSelector);
    }
    else {
        l_IFrame = $("iframe");
    }
        

    var resize = function(p_IFrame) 
    {
        p_IFrame.fadeIn();
        var l_height = 0;

        try {
            l_height = $(p_IFrame.contentWindow.document).outerHeight();
        } catch (e) { }

        if (p_IFrame.data("minheight") != "" && p_IFrame.data("minheight") != null && p_IFrame.data("minheight") != undefined) {
            l_height = p_IFrame.data("minheight");
        }

        if (p_MinimumHeight != 0 && p_MinimumHeight != "" && p_MinimumHeight != null) {
            if (l_height < p_MinimumHeight)
                l_height = p_MinimumHeight;
        }

        p_IFrame.height(l_height);
    };

    resize(l_IFrame);

    
    l_IFrame.load(function () {
        resize($(this));
    });
}