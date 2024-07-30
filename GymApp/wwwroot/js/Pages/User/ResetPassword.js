﻿const handleResetPassword = (event) => {
    event.preventDefault();

    // Obtiene el password 
    const password = $("#newPassword").val()

    //Obtenemos el token de la URl 
    var token = getQueryParam('token');
    console.log("Token:", token);

    // Estructuramos el objeto de ResetPassword para enviar a la API
    const data = {
        token: token,
        newPassword: password
    }

    var apiUrl = API_URL_BASE + "/api/Users/ResetPassword";

            $.ajax({
                headers: {
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                },
                method: "POST",
                url: apiUrl,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                hasContent: true
            }).done(function (data) {
                Swal.fire({
                    title: 'Mensaje',
                    text:   "Your Password have been successfully updated.",
                    icon: 'info'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Redirigir a la página de log in  
                        window.location.href = "/User/Login";
                    }
                });
            }).fail(function (xhr, status, error) {
                let errorMessage = "Unknown error";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                } else if (xhr.responseText) {
                    errorMessage = xhr.responseText;
                } else if (error) {
                    errorMessage = error;
                }
                Swal.fire({
                    title: 'Error!',
                    text: errorMessage,
                    icon: 'error'
                });
            });
};

function getQueryParam(param) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
};

$("#btnResetPassword").click(handleResetPassword);