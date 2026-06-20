import { useEffect, useState } from "react";
import { getCategories } from "../../api/categoryApi";

function CategoryMenu({ onSelectCategory }) {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    const data = await getCategories();
    setCategories(data);
  };

  return (
    <div
      className="
        flex
        flex-wrap
        justify-center
        gap-4
        my-10
      "
    >
      <button
        onClick={() => onSelectCategory(null)}
        className="
          px-5
          py-2
          bg-gray-200
          rounded-full
          hover:bg-gray-300
        "
      >
        Tất cả
      </button>

      {categories.map((category) => (
        <button
          key={category.id}
          onClick={() =>
            onSelectCategory(category.id)
          }
          className="
            px-5
            py-2
            bg-emerald-500
            text-white
            rounded-full
            hover:bg-emerald-600
          "
        >
          {category.name}
        </button>
      ))}
    </div>
  );
}

export default CategoryMenu;