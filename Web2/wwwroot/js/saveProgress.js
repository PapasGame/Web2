// SAVE
/////////////////////////////////////////////////////////////////////

function exportNetwork() {
    var tempNodes = objectToArray(network.getPositions());
    tempNodes.forEach(addProgress);

    var progressJson = JSON.stringify(tempNodes, undefined, 2);////////////////////////////
    $('.hidden_json').val(progressJson); 
    console.log(tempNodes);
}

function addProgress(elem, index) {
    elem.done = getNode(elem.id).options.done;
}


function objectToArray(obj) { //добавляет ID
    return Object.keys(obj).map(function (key) {
        obj[key].id = key;
        return obj[key];
    })
}

function getNode(nodeId) { //возвращает node по id
    var nodeObj = network.body.nodes[nodeId];
    return nodeObj;
}

function getEdge(edgeId) { //возваращет edge по id
    var edgeObj = network.body.edges[edgeId];
    return edgeObj;
}

/////////////////////////////////////////////////////////////