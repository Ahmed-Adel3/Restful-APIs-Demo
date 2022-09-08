let currentPage = 1;
let currentCategory = 0;

window.onload =()=>{
    loadUserSection();
    fillCategoriesDdl();
    getProducts(0,1,10);
    document.addEventListener('scroll', (e)=> {
        getProducts(currentCategory,currentPage+1,10)
    });
}

let changeCategory = (id)=>{
    getProducts(id,1,10);
    currentCategory = id;
}

let confirmDelete =(id)=>{
    let token = localStorage.getItem("token");
    if(!token)
    {
        Swal.fire({
            title: 'Please login first to delete this item!',
            showCancelButton: true,
            confirmButtonText: 'Login',
          })
          .then(result=>{
            if(result.isConfirmed)
                window.location.href = "./login.html"
          })
        return;
    }
    Swal.fire({
        title: 'Are you sure you want to delete this product?',
        showCancelButton: true,
        confirmButtonText: 'Delete',

      }).then((result) => {
        if (result.isConfirmed) {
            fetch(baseUrl+"/api/Products/"+id,
            {
                method:"Delete",
                headers: {
                    "X-Version":"2",
                    'Authorization': `Bearer ${token}`
                }
            })
            .then((response) => response.json())
            .then( result => {
                if(!result.success){
                    Swal.fire({
                        title:"Error Occured",
                        text:result.messages[0],
                        icon:"error"
                    })
                }
                else{
                    document.getElementById("product_"+id).remove();
                    Swal.fire({
                        title:"Success",
                        text:result.result,
                        icon:"success",
                        showConfirmButton: true,
                    })
                }
            })
        }
      })
}

let getProducts = (categoryId, pageNumber, pageSize)=>{
    currentPage = pageNumber;
    fetch(baseUrl+"/api/Products?"+new URLSearchParams({categoryId,pageNumber,pageSize}),
    {
        headers:{"X-Version":"2"}
    })
        .then( result => result.json())
        .then( data => {
            let container = document.getElementById("productsContainer");
            if(pageNumber === 1)
                container.innerHTML = "";

            let products = data.result;
            products.forEach(product => {
                container.innerHTML += 
                `
                    <div class="w-full md:w-1/3 xl:w-1/5 p-6 flex flex-col" id="product_${product.productId}">
                    <a href="">
                    <img class="border-4 rounded-lg hover:grow hover:shadow-lg" src="${baseUrl+product.imageUrl}">
                    <div class="pt-3 flex items-center justify-between">
                    <p class=""> <b>${product.name}</b></p>
                    <a onclick="goToAddEdit(${product.productId})">
                           <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6 text-blue-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"></path>
                           </svg>
                    </a>
                    <a onclick="confirmDelete(${product.productId})">
                           <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6 text-red-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"></path>
                           </svg>
                        </a>
                    </div>
                    <p class="">👉 Quantity: ${product.quantity}</p><p>  👉 Category: ${product.category.name}</p>
                    <p class="pt-1 text-gray-900">👉 Price: £${product.price}</p>
                    </a>
                    </div
                `
            });
        });
}