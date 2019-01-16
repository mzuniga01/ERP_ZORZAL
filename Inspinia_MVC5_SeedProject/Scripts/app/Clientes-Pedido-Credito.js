//ERICK,Wilson JS
$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $('#ModalAgregarClientes').modal('hide');
    //var Comercial = $("#tbCliente_clte_NombreComercial").val()
    $("#tbCliente_clte_Nombres").val(NombreCliente)
    $("#tbCliente_clte_NombreComercial").val(NombreCliente);
    //var Normal = $("#tbCliente_clte_Nombres").val()



    //if (Normal != "**") {
    //    $("#tbCliente_clte_Nombres").val(NombreCliente);

    //    console.log("Si")
    //    console.log(NombreCliente)
    //}

    //console.log(Normal)

    //if (Normal == "**")
    //{
    //    $("#tbCliente_clte_NombreComercial").val(NombreCliente);
    //    console.log(NombreCliente)
      

    //}


});














$(document).on("click", "#tbClienteSC tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    nombreItem = $(this).closest('tr').data('nombre');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $("#tbCliente_clte_Nombres").val(nombreItem);
    $('#ModalAgregarClientes').modal('hide');
    //CargarAsignaciones();


});