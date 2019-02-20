$(document).keydown(function (e) {
    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {
        e.preventDefault();
        //alert("Ctrl-g pressed");
        $("form").submit();
        return false;
    }
    return true;
});