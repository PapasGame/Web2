var colorPicker = document.getElementById('pallete');
colorPicker.addEventListener("input", setColor, false);

function setColor(){
    selectedNodes = network.getSelectedNodes();
    selectedNodes.forEach(function(id){
        node = getNode(id);
        node.color = {
            border: colorPicker.value,
            background: colorPicker.value,
            highlight: {
              border: colorPicker.value,
              background: colorPicker.value
            },
            hover: {
              border: colorPicker.value,
              background: colorPicker.value
            }
          }
          nodes.update({id: id, color: node.color});
    });    

}



function changeSize(){
    selectedNodes = network.getSelectedNodes(); 
    size = document.getElementById('sizeRange').value;
    selectedNodes.forEach(function(id){
        node = getNode(id);
        node.value = Number(size);

        nodes.update({id: id, value: node.value});
    });
}