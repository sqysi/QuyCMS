import axios from "axios";

const API_URL = "https://localhost:7108/api";

export const getProducts = async () => {
  const response = await axios.get(`${API_URL}/products`);
  return response.data;
};
export const getProductById = async (id) => {
  const response =
    await axios.get(`${API_URL}/products/${id}`);

  return response.data;
};