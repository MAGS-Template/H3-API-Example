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
        container.appendChild(colorDiv);
      });
    })
    .catch((error) => console.error("Error:", error));
});
