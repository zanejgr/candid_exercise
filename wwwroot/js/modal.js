function OpenModal(customer_id) {
  const modal = document.getElementById("modal");
  console.log(customer_id);
  modal.style.display = "block";
  fetch(`/address?customerID=${customer_id}`)
    .then((res) => (res.ok ? res.json() : []))
    .then((dat) => fillAddressModal(dat));
}
function CloseModal() {
  const modal = document.getElementById("modal");
  modal.style.display = "none";
}

function fillAddressModal(dat) {
  const addrHeader = document.getElementById("address-header");
  addrHeader.innerHTML = `${dat.length} address(es) found`;

  const addresses = document.getElementById("addresses");
  while (addresses.firstChild) {
    addresses.removeChild(addresses.firstChild);
  }

  console.log(dat);
  [...dat].forEach((element) => {
    const addSpan = document.createElement("div");
    addSpan.className = "address-span";
    const li = document.createElement("dl");

    const AddressLine1 = document.createElement("dt");
    const AddressLine2 = document.createElement("dt");
    const City = document.createElement("dt");
    const State = document.createElement("dt");
    const Zip = document.createElement("dt");
    const Country = document.createElement("dt");

    AddressLine1.innerHTML = "Address Line 1";
    AddressLine2.innerHTML = "Address Line 2";
    City.innerHTML = "City";
    State.innerHTML = "State";
    Zip.innerHTML = "Zip";
    Country.innerHTML = "Country";

    const AddressLine1Val = document.createElement("dd");
    const AddressLine2Val = document.createElement("dd");
    const CityVal = document.createElement("dd");
    const StateVal = document.createElement("dd");
    const ZipVal = document.createElement("dd");
    const CountryVal = document.createElement("dd");

    AddressLine1Val.innerHTML = element.addressLine1.toString();
    AddressLine2Val.innerHTML = (element.addressLine2 ?? "").toString();
    CityVal.innerHTML = element.city.toString();
    StateVal.innerHTML = (element.state ?? "").toString();
    ZipVal.innerHTML = (element.zip ?? "").toString();
    CountryVal.innerHTML = element.country.toString();

    addresses.appendChild(addSpan);
    addSpan.appendChild(li);
    li.appendChild(AddressLine1);
    li.appendChild(AddressLine1Val);
    li.appendChild(AddressLine2);
    li.appendChild(AddressLine2Val);
    li.appendChild(City);
    li.appendChild(CityVal);
    li.appendChild(State);
    li.appendChild(StateVal);
    li.appendChild(Zip);
    li.appendChild(ZipVal);
    li.appendChild(Country);
    li.appendChild(CountryVal);
  });
}
