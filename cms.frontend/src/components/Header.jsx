import { Link } from "react-router-dom";

function Header() {
    return (
        <header className="
            bg-white
            shadow-md
            sticky
            top-0
            z-50
        ">
            <div className="
                max-w-7xl
                mx-auto
                flex
                items-center
                justify-between
                h-20
                px-6
            ">
                <h1 className="
                    text-3xl
                    font-bold
                    text-emerald-500
                ">
<<<<<<< HEAD
                    CMS SHOP
=======
                    QUY CMS SHOP
>>>>>>> 6deb3d7 (cap nhat them frontend)
                </h1>

                <div className="flex gap-8">

                    <Link to="/">
                        Trang chủ
                    </Link>

                    <Link to="/products">
                        Sản phẩm
                    </Link>

                    <Link to="/posts">
                        Bài viết
                    </Link>
<<<<<<< HEAD

                    <Link to="/contact">
                        Liên hệ
                    </Link>

=======
>>>>>>> 6deb3d7 (cap nhat them frontend)
                </div>

                <button className="
                    bg-emerald-500
                    text-white
                    px-4
                    py-2
                    rounded-lg
                ">
                    Giỏ hàng
                </button>
            </div>
        </header>
    );
}

export default Header;