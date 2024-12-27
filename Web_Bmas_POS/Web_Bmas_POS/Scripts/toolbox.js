var APP = APP || {};
APP.UI = (function (el) {
    var spinner;
    function showLoader() {
        var opts = {
            lines: 13,
            length: 7,
            width: 4,
            radius: 10,
            corners: 1,
            rotate: 0,
            color: '#000',
            speed: 1,
            trail: 60,
            shadow: false,
            hwaccel: false,
            className: 'spinner',
            zIndex: 2e9,
            top: '50%',
            left: '50%',
            visibility: true
        };
        //el.spin(opts);
        var target = document.getElementById('spin');
        spinner = new Spinner(opts).spin(target);
        $.blockUI({ message: null });


    }
    function hideLoader(el) {
        $.unblockUI();
        spinner.spin(false);
    }

    return { showLoader: showLoader, hideLoader: hideLoader }
})();

