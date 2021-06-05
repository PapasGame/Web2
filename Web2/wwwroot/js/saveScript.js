     // SAVE
      /////////////////////////////////////////////////////////////////////

      let stringJson;

    function exportNetwork() {
      var tempNodes = objectToArray(network.getPositions());
      tempNodes.forEach(addConnections);
      tempNodes.forEach(addLabel);
      tempNodes.forEach(addColor);
      //tempNodes.forEach(addValue);
      tempNodes.forEach(addDescription);
      tempNodes.forEach(addShape);

      mapName = $('.map-name').text();
      tempNodes.unshift(mapName);
      
        stringJson = JSON.stringify(tempNodes, undefined, 2);

        $('.hidden_json').val(stringJson); 
        $('.hidden_Name').val(mapName); 

      console.log(tempNodes);
    }

    function addConnections(elem, index) { //добавляет соединённые точки
      elem.connections = network.getConnectedNodes(elem.id,'to');
    }

    function addLabel(elem, index){ //добавляет label у node
      elem.label = getNode(elem.id).options.label;
    }

    function addColor(elem, index){ //добавляет color
      elem.color = getNode(elem.id).color;
    }

    function addValue(elem, index){ //добавляет value
      elem.value = getNode(elem.id).value;
}

    function addDone(elem, index) { //добавляет value
        elem.done = getNode(elem.id).options.done;
    }

    function addDescription(elem, index){
      elem.description = getNode(elem.id).description;
    }

    function addShape(elem, index){
      elem.shape = shapeList.get(Number(elem.id));
    }

    function objectToArray(obj) {  //добавляет ID
      return Object.keys(obj).map(function (key) {
        obj[key].id = key;
        return obj[key];
      })
    }

    function getNode(nodeId){  //возвращает node по id
      var nodeObj = network.body.nodes[nodeId];
      return nodeObj;
    }

    function getEdge(edgeId){ //возваращет edge по id
      var edgeObj = network.body.edges[edgeId];
      return edgeObj;
    }

/////////////////////////////////////////////////////////////

function importNetwork() {
      network.unselectAll();
      setDisabled(true);
      $('.nodeLabel').val("");
      $('.nodeDescription').val("");

      var inputData = JSON.parse(stringJson);

      mapName = inputData.shift();
      $('.map-name').text(mapName);
      

      nodes = getNodeData(inputData);
      edges = getEdgeData(inputData);

      var data = {
        nodes: nodes,
        edges: edges,
      }

      network = new vis.Network(mynetwork, data, options);
      addEventsAtNetwork(network);
    }

    function getNodeData(data) { //получает информацию о node из json
      var networkNodes = [];

      data.forEach(function (elem, index, array) {
        networkNodes.push({
          id: elem.id,
          label: elem.label,
          color: elem.color,
          //value: elem.value,
          description: elem.description,
          shape: elem.shape,
          done: elem.done,
          x: elem.x,
          y: elem.y,
        });
      });

      return new vis.DataSet(networkNodes);
    }

    function getNodeById(data, id) {//возвращает node по id из stringJson
      for (var n = 0; n < data.length; n++) {
        if (data[n].id == id) {
          return data[n];
        }
      }
      throw "Cann't find id " + id + "in data";
    }

    function getEdgeData(data) { // получает информацию о edge из json
      var networkEdges = [];

      data.forEach(function (node) {
        node.connections.forEach(function (connId, cIndex, conns) {
          networkEdges.push({ from: node.id, to: connId });
          let cNode = getNodeById(data, connId);

          var elementConnections = cNode.connections;

          var duplicateIndex = elementConnections.findIndex(function (connection) {
            return connection == node.id;
          });

          if (duplicateIndex != -1) {
            elementConnections.splice(duplicateIndex, 1);
          }
        })
      });

      return new vis.DataSet(networkEdges);
    }



    //////////////////////////////////////////////////////////////

