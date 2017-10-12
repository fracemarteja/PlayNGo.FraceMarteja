function post_ajax(settings) {
    settings.type = 'POST';
    settings.dataType = 'json';
    settings.error = function (xhr, ajaxOptions, thrownError) {
        $('.btn.with_spinner').button('reset');
        alert(xhr.statusText);
    };

    $.ajax(settings);
}

function isNullOrWhitespace(input) {
    return (typeof input === 'undefined' || input == null) || input.replace(/\s/g, '').length < 1;
}