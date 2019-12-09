$(document).ready(function () {
    $('.ActiveCheck').change(function () {

        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');
        console.log(id); console.log(value)
        $.ajax({
            url: '/ToDoes/AJAXEdit?id=' + id + '&value=' + value,
            data: {
                id: id,
                value: value

            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        });

    });

});