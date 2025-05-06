//MOOODOS
function toggleTheme() {
    const html = document.documentElement;
    const current = html.getAttribute("data-bs-theme");
    const icon = document.getElementById("themeIcon");

    if (current === "dark") {
        html.setAttribute("data-bs-theme", "light");
        icon.className = "bi bi-moon";
    } else {
        html.setAttribute("data-bs-theme", "dark");
        icon.className = "bi bi-sun";
    }
}




//CARROSEEELLL
var carrossel = document.querySelector('#carouselExample');
var instanciaCarrossel = new bootstrap.Carousel(carrossel, {
    interval: 4000,
    ride: 'carousel'
});

//QUANTIDADE DO CARRINHOOO


/*MENU CADASTRO*/
function menuLogin() {
    const menu = document.getElementById("menu-login");
    menu.classList.toggle("active");
}

//POP UP DO CADASTRO
function abrirCadastro() {
    document.getElementById("menu-cad").classList.add("show");
    document.getElementById("tudo-cad").style.display = "block";
    document.getElemntById("menu-login").style.display = 'none';
    }

    function fecharCadastro() {
     document.getElementById("menu-cad").classList.remove("show");
    document.getElementById("overlay-cad").style.display = "none";
}

//MENSAGEEENSS POOP UP
const popup = document.getElementById('popupLogin');
    popup.style.display = 'block';

    setTimeout(() => {
        popup.style.display = 'none';
    }, 5000); // 5 segundos


const popupCad = document.getElementById('popupCad');
    popupCad.style.display = 'block'; // Exibe o popup

    setTimeout(() => {
        popupCadastro.style.display = 'none'; // Esconde o popup após 5 segundos
    }, 5000); // 5 segundos

//MENUU SACOOLAA
function abrirSacola() {
    document.getElementById('menu-sac').classList.add('aberta');
    document.getElementById('sacola-overlay').style.display = 'block';
}

function fecharSacola() {
    document.getElementById('menu-sac').classList.remove('aberta');
    document.getElementById('sacola-overlay').style.display = 'none';
}

// FAVORITOS
//CAMINHO NO JS NAO PODE TER /~
function toggleFavorito(event, element) {
    event.preventDefault(); // Evita o recarregamento da página

    var img = element.querySelector('img');

    // Caminhos corretos (sem "~")
    var iconeNormal = "/img/favs-icon.png";
    var iconePreenchido = "/img/favsP-icon.png";

    if (img.src.includes("favsP-icon.png")) {
        img.src = iconeNormal;
    } else {
        img.src = iconePreenchido;
    }
}


//SENHAAA VISIVELL
function toggleSenha() {
    var input = document.getElementById("senhaCliente");
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

function toggleSenha2() {
    var input = document.getElementById("senhaCliente2");
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

//MENUUU HAMBURGUERR MOBILEE


function toggleMobileMenu() {
    var menu = document.getElementById("mobileMenu");
    var body = document.body;

    if (menu.classList.contains("show")) {
        menu.classList.remove("show");
        body.style.overflow = "auto"; 
    } else {
        menu.classList.add("show");
        body.style.overflow = "hidden"; 
    }
}

function abrirMenuLogin(event) {
    event.preventDefault(); 
    toggleMobileMenu();     // fecha o menu hambúrguer
    menuLogin();            // exibe o menu de login
}


// Fecha o menu ao clicar em qualquer link dentro dele
/*document.querySelectorAll('#mobileMenu a').forEach(link => {
    link.addEventListener('click', function () {
        toggleMobileMenu(); 
    });

});*/

