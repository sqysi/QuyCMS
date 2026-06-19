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
                    CMS SHOP
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

                    <Link to="/contact">
                        Liên hệ
                    </Link>

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