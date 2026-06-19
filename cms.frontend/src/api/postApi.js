import axios from "axios";

const API_URL = "https://localhost:7108/api";

export const getPosts = async () => {
    const response =
        await axios.get(`${API_URL}/posts`);

    return response.data;
};