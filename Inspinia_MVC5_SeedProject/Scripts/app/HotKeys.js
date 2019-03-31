//Atajos Rapidos

$(window).bind('keydown', function (event) {
   
    if (event.ctrlKey || event.metaKey) {
        switch (String.fromCharCode(event.which).toLowerCase()) {
        case 'b':
            event.preventDefault();
            $('#SelectProduct').click();
            break;
        case 'n':
            event.preventDefault();
            $('#').click();
                break;
        case 'r': 
                //console.log(GetPath())
                var Path = GetPath()
                //Si Path
                if (Path.IsCREATE == -1 && Path.IsEDIT == -1 && Path.Home == 1) {
                    //location.href = "Salida/Create";
                    url = '/' + GetView() + '/Create';
                window.location.replace(url)
                $('#Registrar').click();
                event.preventDefault();
            }
            break;
        case 'l': 
                //console.log(GetPath())
                var Path = GetPath()
                //Si Path
                if (Path.IsCREATE != -1 || Path.IsEDIT != -1 && Path.Home == 1) {
                    location.href = '/'+GetView()+'';
                $('#Registrar').click();
                event.preventDefault();
            }
                break;
        case 'g': 
        var Path = GetPath()
        //Si Path
            if (Path.IsCREATE != -1 || Path.IsEDIT != -1 && Path.Home == 1) {
            $('#submit').click();
            event.preventDefault();
            }
                break;

        case 'e': 
        var Path = GetPath()
        //Si Path
                if (Path.IsCREATE == -1 && Path.IsEDIT == -1 && Path.IsDETAILS == -1 && Path.Home == 1) {
                    GetLastRecord().done(function (data) {
                        location.href = '/' + GetView() + '/Edit/' + data;
                    })
                    event.preventDefault();
                }
                else if (Path.IsCREATE == -1 && Path.IsEDIT == -1 && Path.IsDETAILS != -1 && Path.Home == 1) {
                        id = GetID()
                        location.href = '/'+GetView()+'/Edit/'+id;
                        event.preventDefault();
                 }
            break;

        case 'e': 
        var Path = GetPath()
                //Si Path
            
            break;
        }
    }
});

function GetID() {
    if(GetView() == "Salida" ) {
        tbid = $('#sal_Id').val()
        console.log(tbid)
    }
    return tbid
}

function GetPath() {
    Controlador = GetView(); 
    vurl = window.location.pathname;
    IsCREATE = vurl.search("Create");
    IsEDIT = vurl.search("Edit");
    IsDETAILS = vurl.search("Details");
    Home = vurl.search(Controlador);
    allurl = { IsCREATE, IsEDIT,IsDETAILS, Home }
    console.log(allurl)
    return allurl; 
            
}

function GetView() {
    var vPath = window.location.pathname.toString();
    //str = toString(Path)
    //separador = "/", // un espacio en blanco
    //limite    = 1,
    //arregloDeSubCadenas = Path.split(separador, limite);
    var separador = "/";
    var Vista = vPath.split(separador);
    var vVista = Vista[1].toString()
    return vVista; 
}

function GetLastRecord() {
    var Last = $.ajax({
        url: '/' +GetView()+'/GetLastRecord',
        method: "GET",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            Last = data
        }
    })
    return Last
}

$(function () {
    GetPath()
    //console.log(GetLastRecord())
    GetLastRecord().done(function (data) {
        console.log(data)
    })
 })

//function GetViewTEST() {
//    var vPath = window.location.pathname.toString();
//    //str = toString(Path)
//    //separador = "/", // un espacio en blanco
//    //limite    = 1,
//    //arregloDeSubCadenas = Path.split(separador, limite);
//    var separador = "/";
//    var Vista = vPath.split(separador);
//    var vVista = Vista.toString()
//    console.log(vVista)
//    return vVista; 
//}


//$(function () {
//    GetPath()
//    console.log(GetView())
//    console.log(GetViewTEST())
//})


