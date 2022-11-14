function deletecustomer(id) {
    fetch(`/customer/${id}`,{method:"delete"}).then(()=>{

    const current = {
        col: localStorage.getItem("col")??"id",
        desc: localStorage.getItem("desc")??"false"
    };
    fetch(`/customer?query[0].Field=${current.col}&query[0].Desc=${current.desc}`)
        .then((res)=>res.json())
        .then((dat)=>writetotable(dat));
});
}