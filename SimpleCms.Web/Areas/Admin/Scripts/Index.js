(function () {
    $(function () {

        var _tenantService = abp.services.app.tenant;
        var _$modal = $('#TenantCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate();

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var tenant = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            console.log(tenant);
            
            _tenantService.createTenant(tenant).done(function () {
                _$modal.modal('hide');
                
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });
        
    });
})();
$("body").on("click", ".initLang", function () {
    var name = $(this).data("name");
    var data = {
        tenantName: name
    }
    abp.ajax({
        url: "/Tenants/InitLanguages/",
        data: JSON.stringify(data)
    }).done(function () {
        location.reload(true); //reload page to see new tenant!
    });
});