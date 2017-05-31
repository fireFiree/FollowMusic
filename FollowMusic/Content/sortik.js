"use strict"
    // сортировка таблицы
    // использовать делегирование!
    // должно быть масштабируемо:
    // код работает без изменений при добавлении новых столбцов и строк

    var grid = document.getElementById('grid');
    var flagForId = 1;
    var flagForName = 1;

grid.onclick = function(e) {
    if (e.target.tagName == 'TH') {        
        sortGrid(e.target.cellIndex, e.target.getAttribute('data-type'));
        changeImage(e.target.firstElementChild);
    } else if (e.target.tagName == 'IMG' ) {
        sortGrid(e.target.parentNode.cellIndex, e.target.getAttribute('data-type'));
        changeImage(e.target);
    }
    else return;
};

function changeImage(e) {
    if (e.hasAttribute('id','sort')) {
        if (e.src == "http://localhost:1290/Content/arrowDown.jpg") {
            e.src = "http://localhost:1290/Content/arrowUp.jpg";
        } else {
            e.src = "http://localhost:1290/Content/arrowDown.jpg";
        }
    }
    else
        return;
}


function sortGrid(colNum, type) {
    var tbody = grid.getElementsByTagName('tbody')[0];

    // Составить массив из TR
    var rowsArray = [].slice.call(tbody.rows);

    // определить функцию сравнения, в зависимости от типа
    var compare;

    switch (type) {
        case 'number':
            flagForId++;
            if (flagForId % 2 == 0)
            {
                compare = function (rowA, rowB) {
                    return Number(rowA.cells[colNum].innerHTML) - Number(rowB.cells[colNum].innerHTML);
                };
            }
            else
            compare = function(rowA, rowB) {
                return Number(rowB.cells[colNum].innerHTML) - Number(rowA.cells[colNum].innerHTML);
            };
            break;
        case 'string':
            flagForName++;
            if (flagForName % 2 == 0) {
                compare = function (rowA, rowB) {
                    return rowB.cells[colNum].innerHTML > rowA.cells[colNum].innerHTML ? 1 : -1;
                };
            }
            else
            compare = function(rowA, rowB) {
                return rowA.cells[colNum].innerHTML > rowB.cells[colNum].innerHTML ? 1 : -1;
            };
            break;
    }

    // сортировать
    rowsArray.sort(compare);

    // Убрать tbody из большого DOM документа для лучшей производительности
    grid.removeChild(tbody);

    // добавить результат в нужном порядке в TBODY
    // они автоматически будут убраны со старых мест и вставлены в правильном порядке
    for (var i = 0; i < rowsArray.length; i++) {
        tbody.appendChild(rowsArray[i]);
    }

    grid.appendChild(tbody);

}
