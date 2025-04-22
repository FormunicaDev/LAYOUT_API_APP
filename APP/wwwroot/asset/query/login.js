$(document).ready(function(){
    var OSName="Desconocido";
    if (navigator.appVersion.indexOf("Win")!=-1) OSName="Windows";
    if (navigator.appVersion.indexOf("Mac")!=-1) OSName="MacOS";
    if (navigator.appVersion.indexOf("X11")!=-1) OSName="UNIX";
    if (navigator.appVersion.indexOf("Linux")!=-1) OSName="Linux";
    if (navigator.appVersion.indexOf("Android")!=-1) OSName="Android";

    $('#sistema').val(OSName);
    $('#navegador').val(bowser.name, bowser.version);
});

$('#btnLogin').click(()=>{
    let user = $('#user').val()
    let pass = $('#pass').val()
    const passHash = CryptoJS.SHA256(pass)
    console.log(passHash)
    $('#passHash').val(passHash)

    let datos=$('#frmDatos').serialize();

    if(user === "") {
        alertify.error("Ingrese su usuario")
        return false
    }
    if(pass === "") {
        alertify.error("Ingrese su Contraseña")
        return false
    }
    else {
        
        $.ajax({
            url:"Controller/login.php",
			method:"POST",
			data:datos,
			cache:false,
			beforeSend:function(){
				$('#btnLogin').text("Accediendo...");
			},
            success:function(data){
                if(data==="1") {
                    $(location).attr('href','inicio.php')
                } else {
                    alertify.alert("Error de acceso","Usuario o contraseña incorrecta");
					$('#btnLogin').text("Iniciar sesión");
					$('#pass').val('');
					return false;
                }
            }
        })
        return false;
    }
})