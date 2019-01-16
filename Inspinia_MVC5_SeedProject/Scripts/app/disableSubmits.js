var tmrReady = setInterval(isPageFullyLoaded, 100);

function isPageFullyLoaded() {
    if (document.readyState == "loaded" || document.readyState == "complete") {
        subclassForms();
        clearInterval(tmrReady);
    }
}

function submitDisabled(_form, currSubmit) {
    return function () {
        var mustSubmit = true;
        if (currSubmit != null)
            mustSubmit = currSubmit();

        var els = _form.elements;
        for (var i = 0; i < els.length; i++) {
            if (els[i].type == "submit")
                if (mustSubmit)
                    [i].disabled = true;
        }
        return mustSubmit;
    }
}

function subclassForms() {
    for (var f = 0; f < document.forms.length; f++) {
        var frm = document.forms[f];
        frm.onsubmit = submitDisabled(frm, frm.onsubmit);
    }
}