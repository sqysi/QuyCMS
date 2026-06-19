import { BrowserRouter, Routes, Route } from "react-router-dom";

import Home from "./pages/Home";
import Products from "./pages/Products";
import Posts from "./pages/Posts";
import ProductDetail from "./pages/ProductDetail";
function App() {
    return (
        <BrowserRouter>
            <Routes>

                <Route
                    path="/"
                    element={<Home />}
                />

                <Route
                    path="/products"
                    element={<Products />}
                />

                <Route
                    path="/posts"
                    element={<Posts />}
                />
                <Route
                    path="/products/:id"
                    element={<ProductDetail />}
                />

            </Routes>
        </BrowserRouter>
    );
}

export default App;