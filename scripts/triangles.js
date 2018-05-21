function document_ready() {
    //drawTriangle("A", 1);
}

function drawTriangle_onclick() {
    var row = txtRow.value;
    var col = eval(txtColumn.value);

    //value validation would be nice hear :)
    drawTriangle(row, col);
}

function drawTriangle(row, col) {
    //var coords = [[0, 0], [0, 10], [10, 10]];  //example return data
    var coords = getCoordinatesFromService(row, col);

    var svg = document.getElementById("svgTriangles");
    var triangle = document.createElementNS("http://www.w3.org/2000/svg", "polygon");
    for (coord of coords) {
        var point = svg.createSVGPoint();
        point.x = coord[0];
        point.y = coord[1];
        triangle.points.appendItem(point);
        
    }
    triangle.setAttribute("stroke", "#eee");
    triangle.setAttribute("stroke-width", "1");
    triangle.setAttribute("fill", "blue");

    svg.appendChild(triangle);
}


function getCoordinatesFromService(row, col) {
    var arrRet = [];
    var dataObj = {};
    dataObj["row"] = row;
    dataObj["col"] = col;
    debugger;
    
    var sURL = "http://localhost/triangle/TriangleService.svc/GetCoordinates"

    $.ajax({
        async: false,
        type: "POST",
        url: sURL,
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(dataObj)
    }).done(function (retData) {
        debugger;
        arrRet = JSON.parse(retData.GetCoordinatesResult);
    }).fail(function (XMLHttpRequest, textStatus, errorThrown) {
        alert("error: " + errorThrown);
    });
    return arrRet;
}