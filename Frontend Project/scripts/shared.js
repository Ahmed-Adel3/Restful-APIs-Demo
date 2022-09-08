let baseUrl = "https://localhost:44365";

let fillCategoriesDdl = ()=>{
    fetch(baseUrl+"/api/Categories",
    {
        headers:{"X-Version":"2"}
    })
        .then( result => result.json())
        .then( data => {
            let ddlItem = document.getElementById("categoriesDdl");
            let categories = data.result;
            categories.forEach(cat => {
                ddlItem.innerHTML += `<option value='${cat.categoryId}'" >${cat.name} </option>`
            })
            
        })
}

let logout = ()=>{
    localStorage.removeItem("token");
    localStorage.removeItem("name")
    window.location.href = "./Index.html";
}

let loadUserSection = ()=>{
    let loginBtn = document.getElementById("loginBtn");
    let welcomePlaceholder = document.getElementById("welcomePlaceholder");
    let logoutBtn = document.getElementById("logoutBtn");

    if(localStorage.getItem("token"))
    {
        loginBtn.hidden = true;
        logoutBtn.hidden = false;
        welcomePlaceholder.innerHTML = "Welcome, "+localStorage.getItem("name")
    }
    else{
        loginBtn.hidden = false;
        logoutBtn.hidden = true;
    }
}

let register = ()=>{
    let Email = document.getElementById("email").value
    let Password = document.getElementById("password").value
    let FullName = document.getElementById("name").value

    fetch(`${baseUrl}/api/Account/Register`,
    {
        method:"POST",
        body: JSON.stringify({FullName,Password,Email}),
        headers: {
            "X-Version":"2",
            'Content-Type': 'application/json'
          },
    })
        .then(response => response.json())
        .then (result =>{
            if(!result.success){
                Swal.fire({
                    title:"Error Occured",
                    text:result.messages[0],
                    icon:"error"
                })
            }
            else{
                login();
            }
        })
}

let login = ()=>{
    let mail = document.getElementById("email").value
    let pass = document.getElementById("password").value

    fetch(`${baseUrl}/api/Account/Login?Email=${mail}&Password=${pass}`,
    {
        headers:{"X-Version":"2"}
    })
        .then(response => response.json())
        .then (result =>{
            if(!result.success){
                Swal.fire({
                    title:"Error Occured",
                    text:result.messages[0],
                    icon:"error"
                })
            }
            else{
                localStorage.setItem("token",result.result.token);
                localStorage.setItem("name",result.result.fullName)
                window.location.href = "./Index.html";
            }
        })
}

let goToAddEdit = (id)=>{
    let action = id?"edit":"add"
    let token = localStorage.getItem("token");
    if(!token)
    {
        Swal.fire({
            title: `Please login first to ${action} item!`,
            showCancelButton: true,
            confirmButtonText: 'Login',
          })
          .then(result=>{
            if(result.isConfirmed)
                window.location.href = "./login.html"
          })
        return;
    }

    window.location.href = "./add-edit-product.html"+((id)?"?productId="+id:"");

}

