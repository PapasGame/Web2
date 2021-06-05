var editing = false;

$(document).ready(function(){
    var $toggleButton = $('.toggle-button'),
        $menuWrap = $('.menu-wrap');

    $toggleButton.on('click', function(){
        $(this).toggleClass('button-open');
        $menuWrap.toggleClass('menu-show');
    });

    $('.nodeLabel').focusout(function(){
        nodeId = network.getSelectedNodes()[0];
        node = getNode(nodeId);
        // node.options.label = $(this).val();
        nodes.update({id: nodeId, label: $(this).val()});
    });

    $('.nodeDescription').focusout(function(){
        nodeId = network.getSelectedNodes()[0];
        node = getNode(nodeId);
        // node.description = $(this).val();
        nodes.update({id: nodeId, description: $(this).val()});
    });

    setDisabled(true);

    $('.map-name').dblclick(function(){
        if(editing) return;
        editing = true;

        $oldText = $(this).html();
        $(this).html("");

        $(this).append($('<input>', {
            class: 'input-map-name',
            type: 'text',
            value: $oldText,
            width: (($oldText.length)*1.63) + 'ch'
        }));

        $(this).css('z-index', '99999');
        $('#overlay').fadeIn(300);

        $(".input-map-name").on("input",resizeInput);
        $(".input-map-name").on("focusout",closeNameEditing);
        $(".input-map-name").on("keydown", function(e){
            if(e.key == "Enter"){
                closeNameEditing();
            };
        });
                
        $(".input-map-name").focus();
    })

});

function removeDark(){
    $('#overlay').fadeOut(300, function(){
        $('.map-name').css('z-index', '1');
    })
}

function closeNameEditing(){
    $input = $('.input-map-name');
        $newName = $input.val();
        $('.input-map-name').remove();
        $('.map-name').html($newName);
        editing = false;
        removeDark();
}

function resizeInput(){
    this.style.width = ((this.value.length)*1.63) + 'ch';
}

function setDisabled(flag){
    $('#pallete').val("#000000");
    $('#pallete').prop("disabled", flag);
    $('#sizeRange').prop("disabled", flag);
    $('.nodeLabel').prop("disabled", flag);
    $('.nodeDescription').prop("disabled", flag);
}










