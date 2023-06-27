$(function () {
    var placeholder = $("#modal-placeholder");
    $(document).on('click','button[data-toggle="ajax-modal"]',function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            beforeSend: function () { ShowLoading(); },
            complete: function () { $("body").preloader('remove'); },
            error: function () {
                ShowSweetErrorAlert();
            }
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });

    placeholder.on('click', 'button[data-save="modal"]', function () {
        ShowLoading();
        var form = $(this).parents(".modal").find('form');
        var actionUrl = form.attr('action');
        var dataToSend = new FormData(form.get(0));

        $.ajax({
            url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
                ShowSweetErrorAlert();
            }}).done(function (data) {
                var newBody = $(".modal-body", data);
                var newFooter = $(".modal-footer", data);
                placeholder.find(".modal-body").replaceWith(newBody);
                placeholder.find(".modal-footer").replaceWith(newFooter);

            var IsValid = newBody.find("input[name='IsValid']").val() === "True";
            if (IsValid) {
                $.ajax({ url: '/admin/showmassage/index', error: function () { ShowSweetErrorAlert(); } }).done(function (dataa) {
                    ShowSweetSuccessAlert(dataa)
                });

                $table.bootstrapTable('refresh')
                placeholder.find(".modal").modal('hide');
            }
        });

        $("body").preloader('remove');
    });
});

function ShowSweetErrorAlert() {
    Swal.fire({
        type: 'error',
        title: 'خطایی رخ داده است !!!',
        text: 'لطفا تا برطرف شدن خطا شکیبا باشید.',
        confirmButtonText: 'بستن'
    });
}

function ShowLoading() {
    $("body").preloader({ text: 'لطفا صبر کنید ...' });
}

//function ShowSweetSuccessAlert(message) {
//    Swal.fire({
//        position: 'top-middle',
//        type: 'success',
//        title: message,
//        confirmButtonText: 'بستن',
//    })
//}
function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}
var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};
function ShowSweetSuccessAlert(dataa) {
    if (dataa.isSucceeded) {
        Swal.fire({

            position: 'top-middle',
            type: 'success',
            title: dataa.message,
            confirmButtonText: 'بستن',
        })
    }
    else {
        Swal.fire({
            type: 'error',
            title: dataa.message,
            confirmButtonText: 'بستن'
        });
    }
    
}

