import { BrowserRouter, Routes, Route } from "react-router-dom";

import Home from "./pages/Home";
import Products from "./pages/Products";
import Posts from "./pages/Posts";
import ProductDetail from "./pages/ProductDetail";
<<<<<<< HEAD
=======
import PostDetail from "./pages/PostDetail";
>>>>>>> 6deb3d7 (cap nhat them frontend)
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
<<<<<<< HEAD

            </Routes>
=======
                <Route
                    path="/posts/:id"
                    element={<PostDetail />}
                />
            </Routes>

>>>>>>> 6deb3d7 (cap nhat them frontend)
        </BrowserRouter>
    );
}

export default App;