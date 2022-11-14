
function SortBy(colname) {
    const current = {
        col: localStorage.getItem("col"),
        desc: localStorage.getItem("desc")??"false"
    };
    console.log(current);
    console.log(colname);

    const next = {
        col: colname.toLowerCase(),
        desc: (colname.toLowerCase()===current.col)&&!JSON.parse(current.desc)
    };
    console.log(next);
    localStorage.setItem("col",next.col);
    if(next.desc){
        localStorage.setItem("desc","true");
    }else{
        localStorage.removeItem("desc");
    }

    fetch(`/customer?query[0].Field=${colname}&query[0].Desc=${next.desc}`)
        .then((res)=>res.json())
        .then((dat)=>writetotable(dat));
}

function writetotable(dat){
    const tbod = document.getElementById('tbod');
    while(tbod.firstChild){
        tbod.removeChild(tbod.firstChild);
    }
    for(let i = 0; i < dat.length; i++){
        let tr = tbod.insertRow(-1);
        let cell = tr.insertCell();
        cell.innerHTML = dat[i].id;
        cell = tr.insertCell();
        cell.innerHTML = dat[i].firstName;
        cell = tr.insertCell();
        cell.innerHTML = dat[i].lastName;
        tr.onclick = ()=>OpenModal(dat[i].id);
    }
}