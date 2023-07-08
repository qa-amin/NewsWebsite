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

$(document).on('click', 'button[data-save="Ajax"]', function () {
    var form = $(".newsletter-widget").find('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));

    $.ajax({
        url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
            ShowSweetErrorAlert();
        }
    }).done(function (data) {
        var newForm = $("form", data);
        $(".newsletter-widget").find("form").replaceWith(newForm);
        var IsValid = newForm.find("input[name='IsValid']").val() === "True";
        if (IsValid) {
            $.ajax({
                url: '/admin/showmassage/index', error: function () { ShowSweetErrorAlert(); } }).done(function (notification) {
                ShowSweetSuccessAlert(notification)
            });
        }
    });
});
function ActiveOrInActiveNewsletter(email) {
    $.ajax({
        url: "/Admin/Newsletter/ActiveOrInActiveMembers?email=" + email,
        beforeSend: function () { ShowLoading(); },
        complete: function () { $("body").preloader('remove'); },
        type: "get",
        data: {},
    }).done(function (result) {
        if (result != "Success")
            ShowErrorMessage(result);
    });
}
function ConfigureSettings(id, action) {
    $.ajax({
        url: "/Admin/UserManager/" + action + "?userId=" + id,
        beforeSend: function () { ShowLoading(); },
        complete: function () { $("body").preloader('remove'); },
        type: "get",
        data: {},
    }).done(function (result) {
        if (result == "فعال" || result == "تایید شده" || result == "قفل نشده") {
            $("#" + action).removeClass("badge-danger").addClass("badge-success");
            $("#btn" + action).removeClass("btn-suceess").addClass("btn-danger");
            if (result == "فعال")
                $("#btn" + action).html("غیرفعال شود");
            else if (result == "قفل نشده")
                $("#btn" + action).html("قفل شود");
            else
                $("#btn" + action).html("تایید نشود");
        }

        else {
            $("#" + action).removeClass("badge-success").addClass("badge-danger");
            $("#btn" + action).removeClass("btn-danger").addClass("btn-success");
            if (result == "غیرفعال")
                $("#btn" + action).html("فعال شود");
            else if (result == "قفل شده")
                $("#btn" + action).html("قفل نشود");
            else
                $("#btn" + action).html("تایید شود");
        }
        $("#" + action).html(result);
    });
}

function Bookmark(newsId) {
    $.ajax({
        url: "/Home/BookmarkNews?newsId=" + newsId,
    }).done(function (result) {
        if (result == true) {
            $("#bookmark").html('<i aria-hidden="true" class="fa fa-bookmark"></i>');
        }
        else if (result == false) {
            $("#bookmark").html('<i aria-hidden="true" class="fa fa-bookmark-o"></i>');
        }
        else {
            $("#modal-placeholder").html(result);
            $("#pills-tab").after("<p class='alert alert-danger'>برای بوکمارک کردن خبر ابتدا باید وارد سایت شوید.</p>")
            $("#modal-placeholder").find('.modal').modal('show');
        }
    });
}

function LikeOrDisLike(newsId, isLiked) {
    $.ajax({
        url: "/Home/LikeOrDisLike?newsId=" + newsId + "&&isLike=" + isLiked,
    }).done(function (result) {
        $("#like").html(result.like);
        $("#dislike").html(result.dislike);
    });
}

function SendComment(parentCommentId) {
    var form = $("#reply-" + parentCommentId).find('form');
    var actionUrl = form.attr('action');
    var dataToSend = new FormData(form.get(0));
    var loaderAfter = "#comment-" + parentCommentId;
    if ($("#comment-" + parentCommentId).length == 0) {
        loaderAfter = "#reply-"
    }
    $.ajax({
        url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
            ShowSweetErrorAlert();
        },
        beforeSend: function () {
            $(".vizew-btn").attr("disabled", true);
            $(loaderAfter).after("<p class='text-center mb-5 mt-3'><span style='font-size:18px;font-family: Vazir_Medium;'> در حال ارسال دیدگاه  </span><img src='/icons/LoaderIcon.gif'/></p>")
        },
        complete: function () {
            $(".vizew-btn").attr("disabled", false);
            $(loaderAfter).next().replaceWith("");
        }
    }).done(function (data) {
        var newForm = $("form", data);
        $("#reply-" + parentCommentId).find("form").replaceWith(newForm);
        var IsValid = newForm.find("input[name='IsValid']").val() === "True";
        if (IsValid) {
            $("#comment-" + parentCommentId).next().replaceWith("");
            $("#comment-" + parentCommentId).next().replaceWith("");
            $.ajax({ url: '/Admin/Base/Notification', error: function () { ShowSweetErrorAlert(); } }).done(function (notification) {
                ShowSweetSuccessAlert(notification)
            });
            $("#Name").val("");
            $("#Email").val("");
            $("#Desription").val("");
        }
    });
}

function HideCommentForm(parentCommentId, newsId) {
    $("#comment-" + parentCommentId).next().replaceWith("");
    $("#comment-" + parentCommentId).next().replaceWith("");
    $("#btn-" + parentCommentId).html("پاسخ");
    $("#btn-" + parentCommentId).attr("onclick", "ShowCommentForm('" + parentCommentId + "')");
}

function ShowCommentForm(parentCommentId, newsId) {
    $.ajax({
        url: "/Admin/Comments/SendComment?parentCommentId=" + parentCommentId + "&&newsId=" + newsId,
        beforeSend: function () { $("#comment-" + parentCommentId).after("<p class='text-center mb-5 mt-3'><span style='font-size:18px;font-family: Vazir_Medium;'> لطفا منتظر بماند  </span><img src='/icons/LoaderIcon.gif'/></p>") },
        error: function () {
            ShowSweetErrorAlert();
        }
    }).done(function (result) {
        $("#comment-" + parentCommentId).next().replaceWith("");
        $("#comment-" + parentCommentId).after("<hr/>" + result);
        $("#btn-" + parentCommentId).html("لغو پاسخ");
        $("#btn-" + parentCommentId).attr("onclick", "HideCommentForm('" + parentCommentId + "','" + newsId + "')");
    });
}

$(document).on('click', 'a[data-toggle="tab"]', function () {
    var url = $(this).data('url');
    var id = $(this).attr('id');
    var contentDivId = "#MostViewedNewsDiv";
    var loadingDivId = "#nav-mostViewedNews";
    if ($(this).hasClass("most-talk")) {
        contentDivId = "#MostTalkNewsDiv";
        loadingDivId = "#nav-mostTalkNews";
    }

    $.ajax({
        url: url,
        beforeSend: function () { $(loadingDivId).html("<p class='text-center mb-5 mt-3'><span style='font-size:18px;font-family: Vazir_Medium;'>در حال بارگزاری اطلاعات خبر </span><img src='/icons/LoaderIcon.gif'/></p>") },
        error: function () {
            ShowSweetErrorAlert();
        }
    }).done(function (result) {
        $(contentDivId).html(result);
        $(contentDivId + " a").removeClass("active");
        $("#" + id).addClass("active");
    });
});