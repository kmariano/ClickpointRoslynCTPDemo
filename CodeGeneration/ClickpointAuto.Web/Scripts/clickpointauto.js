$(document).ready(function () {

    $("#optionsdialog").dialog({
        autoOpen: false,
        resizable: false,
        height: 300,
        width: 380,
        title: 'Car Package Options',
        modal: true,
        buttons: [
                  {
                      text: "Close",
                      id: "Close",
                      click: function () {
                          $(this).dialog("close");
                      }
                  }
                 ]
    });

    $('input[name="showOptionsBtn"]').click(function () {
        var id = $(this).attr('id');
        var url = '/car/showoptions?id=' + id;

        $.ajax(
                       {
                           url: url,
                           type: 'GET',
                           async: false,
                           dataType: 'json',
                           contentType: 'application/json; charset=utf-8',
                           success: function (response) {

                               var optionsDialog = $('#optionsdialog');
                               $('div#optionsdialog > ol#caroptions > li').remove();
                               var optionsList = $('#optionsdialog').children('ol').first();
                               $.each(response, function (index, option) {
                                   var optionItem = '<li>' + option + '</li>';
                                   $(optionsList).append(optionItem);
                               });

                               $('#optionsdialog').dialog('open');
                           }
                       }
                    );

    });

});