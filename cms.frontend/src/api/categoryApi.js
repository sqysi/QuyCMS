import axios from "axios";

const API_URL = "https://localhost:7108/api";

export const getCategories = async () => {
  const response = await axios.get(
    `${API_URL}/categoryproducts`
  );

  return response.data;
};
