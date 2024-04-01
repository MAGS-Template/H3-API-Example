document.addEventListener("DOMContentLoaded", function () {
  fetch("https://h3-api.onrender.com/api/RGBDatas")
    .then((response) => response.json())
    .then((data) => {
      const container = document.getElementById("color-container");
      data.forEach((item) => {
        const colorDiv = document.createElement("div");
        colorDiv.style.backgroundColor = `rgb(${item.red}, ${item.green}, ${item.blue})`;
        colorDiv.style.width = "100px";
        colorDiv.style.height = "100px";
        colorDiv.style.margin = "10px";
        colorDiv.style.display = "flex";
        colorDiv.style.justifyContent = "center";
        colorDiv.style.alignItems = "center";
        colorDiv.style.color = "white";
        colorDiv.style.fontSize = "16px";
        colorDiv.style.borderRadius = "5px";
        colorDiv.style.boxShadow = "0 0 10px rgba(0,0,0,0.5)";
        colorDiv.textContent = `Owner ID: ${item.ownerId}`;
        container.appendChild(colorDiv);
      });
    })
    .catch((error) => console.error("Error:", error));
});

function updatePreview() {
  const red = document.getElementById("red").value;
  const green = document.getElementById("green").value;
  const blue = document.getElementById("blue").value;
  const preview = document.getElementById("color-preview");
  preview.style.backgroundColor = `rgb(${red}, ${green}, ${blue})`;
}

document.getElementById("red").addEventListener("input", updatePreview);
document.getElementById("green").addEventListener("input", updatePreview);
document.getElementById("blue").addEventListener("input", updatePreview);

document
  .getElementById("color-form")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    const red = document.getElementById("red").value;
    const green = document.getElementById("green").value;
    const blue = document.getElementById("blue").value;
    const ownerId = document.getElementById("ownerId").value;

    const newColorData = {
      red: parseInt(red),
      green: parseInt(green),
      blue: parseInt(blue),
      ownerId: parseInt(ownerId),
    };

    fetch("https://h3-api.onrender.com/api/RGBDatas", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newColorData),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Success:", data);
        const container = document.getElementById("color-container");
        const colorDiv = document.createElement("div");
        colorDiv.style.backgroundColor = `rgb(${data.red}, ${data.green}, ${data.blue})`;
        colorDiv.style.width = "100px";
        colorDiv.style.height = "100px";
        colorDiv.style.margin = "10px";
        colorDiv.style.display = "flex";
        colorDiv.style.justifyContent = "center";
        colorDiv.style.alignItems = "center";
        colorDiv.style.color = "white";
        colorDiv.style.fontSize = "16px";
        colorDiv.style.borderRadius = "5px";
        colorDiv.style.boxShadow = "0 0 10px rgba(0,0,0,0.5)";
        colorDiv.textContent = `Owner ID: ${data.ownerId}`;
        container.appendChild(colorDiv);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  });
function populateOwnerDropdown() {
  fetch("https://h3-api.onrender.com/api/Users")
    .then((response) => response.json())
    .then((users) => {
      const ownerIdSelect = document.getElementById("ownerId");
      users.forEach((user) => {
        const option = document.createElement("option");
        option.value = user.id;
        option.textContent = user.id + ": " + user.username;
        ownerIdSelect.appendChild(option);
      });
    })
    .catch((error) => console.error("Error fetching users:", error));
}

document.addEventListener("DOMContentLoaded", populateOwnerDropdown);
