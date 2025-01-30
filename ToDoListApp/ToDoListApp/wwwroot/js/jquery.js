$(document).ready(function () {



    $('button[name="action"][value="save"').hide(); // sakladım



    console.log("merhaba");

    $('button[name="action"][value="edit"]').click(function () {
        var itemId = $(this).data('id'); // item ID

        // Span'ı dinamik olarak seç
        var spanElement = $(`#secretspan_${itemId}`);
        var currentText = spanElement.text(); // Mevcut metni al
        $(`#itemId_${itemId}`).val(itemId);
        // İlgili input öğesini bul
        var inputElement = $(`#newInput_${itemId}`);

        // Span'ı gizle
        spanElement.hide();

        // Input öğesini görünür yap ve değerini span'den al
        inputElement.attr('type', 'text').val(currentText).show();

        // Input'a odaklan ve tüm metni seç
        inputElement.focus().select();
        $('button[name="action"][value="edit"]').hide();
        $('button[name="action"][value="save"').show();

        // Input'tan çıkıldığında (blur) tekrar span'ı göster
       
    });



    $(".daire").click(function () {

        var itemId = $(this).closest("form").find("input[type='hidden']").val();
        // itemId'yi console'a yazdır
        console.log(itemId);
        $("#idState").val(itemId);
        

        $(".statemenu").css("display", "block");

        $()

    });

    
});