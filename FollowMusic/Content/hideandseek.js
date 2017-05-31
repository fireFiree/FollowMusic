window.onload = function () {
        document.getElementById('toggler').onclick = function () {
            openbox('box', this);
            return false;
        };
    document.getElementById('toggler1').onclick = function () {
            openbox('box1', this);
            return false;
        };
    };
    function openbox(id, toggler) {
        var div = document.getElementById(id);
        if (div.style.display == 'block') {
            div.style.display = 'none';
            toggler.innerHTML = 'Показать';
        }
        else {
            div.style.display = 'block';
            toggler.innerHTML = 'Скрыть';
        }

    }


   
        

