$("input[name='phone']").bind("keypress", function (e) {
    var keyCode = e.which ? e.which : e.keyCode

    if (!(keyCode >= 48 && keyCode <= 57)) {
        return false;
    }
    else {
        return true;
    }
});

var rules = {
    username: {
        required: true,
        minlength: 3,
        maxlength: 10
    },
    fullname: "required",
    email: {
        required: true,
        email: true
    },
    password: {
        required: true,
        minlength: 6
    },
    password_new: {
        required: true,
        minlength: 6
    },
    confirm_password_new: {
        equalTo: "#password_new"
    },
    password_confirm: {
        equalTo: "#password"
    },
    phone: {
        required: true,
        maxlength: 10,
        minlength: 7
    },
};

var messages = {
    username: {
        required: "Bạn cần nhập tài khoản",
        minlength: "Tài khoản tối thiểu 3 ký tự",
        maxlength: "Tài khoản tối đa 3 ký tự"
    },
    email: {
        required: "Bạn cần nhập email",
        email: "Email không hợp lệ"
    },
    password: {
        required: "Bạn cần nhập mật khẩu",
        minlength: "Mật khẩu tổi thiểu 6 ký tự"
    },
    password_new: {
        required: "Bạn cần nhập mật khẩu mới",
        minlength: "Mật khẩu tổi thiểu 6 ký tự"
    },
    fullname: "Bạn cần nhập tên của bạn",
    password_confirm: {
        equalTo: "Bạn cần nhập lại mật khẩu đã nhập"
    },
    confirm_password_new: {
        equalTo: "Bạn cần nhập lại mật khẩu mới đã nhập"
    },
    phone: {
        required: "Bạn cần nhập số điện thoại",
        minlength: "Số điện thoại không đúng định dạng",
        maxlength: "Số điện thoại không đúng định dạng"
    }
};

var options = {
    errorElement: 'span',
    errorClass: 'help-block text-danger',
    validClass: "success",
    highlight: function (element) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).removeClass('is-invalid');
    },
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
};

var optionsDatabase = {
    paging: true,
    searching: true,
    pageLength: 10,
    processing: true,
    serverSide: false,
    "bSort": false,
    "responsive": true
}