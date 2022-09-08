let productId;
window.onload =()=>{
    loadUserSection();
    const params = new URLSearchParams(window.location.search)
    productId = params.get("productId");
    fillCategoriesDdl();

    if(productId)
       initEditPage()
}

let initEditPage = () =>{
  document.getElementById("title").innerHTML = "Edit product";
  document.getElementById("bannerTitle").innerHTML = "Edit product";

    fetch(baseUrl+"/api/Products/"+productId,{
      headers:{"X-Version":"2"}
    })
       .then( result => result.json())
       .then( data => {
            let product = data.result;
            document.getElementById("productName").value = product.name;
            document.getElementById("productPrice").value = product.price;
            document.getElementById("productQuantity").value = product.quantity;
            document.getElementById("categoriesDdl").value = product.category.categoryId
            document.getElementById("imageDiv").hidden = false;
            document.getElementById("productImagePreview").src = baseUrl+product.imageUrl
            document.getElementById("formTitle").innerHTML = `Edit product [ ${product.name} ]`;
            document.getElementById("submitButton").innerHTML = "Edit Product";
       })
}

async function AJAXSubmit (oFormElement) {
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
    const model = new FormData(oFormElement);  
    let url = baseUrl+"/api/Products"+ ((productId)?"/"+productId:"");
    fetch(url, 
    {
        method: (productId)?'PUT':'POST', 
        headers: {
            "X-Version":"2",
            'Authorization': `Bearer ${token}`
          },
          body: model,
    })  
    .then((response) => response.json())
    .then((result) => {
      console.log('Result:', result);
      if(!result.success){
        Swal.fire({
            title:"Error Occured",
            text:result.messages[0],
            icon:"error"
        })
      } else{
        Swal.fire({
            title:"Success",
            text:result.result,
            icon:"success",
            showConfirmButton: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                window.location.href = "./Index.html";
            }
        })
      }

    })
    .catch((error) => {
       Swal.fire({
            title:"Error Occured",
            text:error,
            icon:"error"
       })
    });
  }
  