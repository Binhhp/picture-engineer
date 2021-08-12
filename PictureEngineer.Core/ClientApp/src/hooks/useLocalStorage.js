import { useState } from "react";

// Hook
const useLocalStorage = (key, initialValue) => {
    // Trạng thái để lưu trữ giá trị của chúng ta
    // Truyền hàm trạng thái ban đầu cho useState để logic chỉ được thực thi một lần
    const [storedValue, setStoredValue] = useState(() => {
      try {
        // Lấy từ bộ nhớ cục bộ bằng khóa
        const item = window.localStorage.getItem(key);
        // Phân tích cú pháp json được lưu trữ hoặc nếu không trả về initialValue
        return item ? JSON.parse(item) : initialValue;
      } catch (error) {
       // Nếu lỗi cũng trả về giá trị ban đầu
        console.log(error);
        return initialValue;
      }
    });
    // Trả về một phiên bản gói của hàm setter của useState ...
    // ... duy trì giá trị mới cho localStorage.
    const setValue = (value) => {
      try {
        // Cho phép giá trị là một hàm để chúng ta có cùng một API như useState
        const valueToStore =
          value instanceof Function ? value(storedValue) : value;
        // Giữ trạng thái
        setStoredValue(valueToStore);
        // Lưu vào bộ nhớ cục bộ
        window.localStorage.setItem(key, JSON.stringify(valueToStore));
      } catch (error) {
        // Một triển khai nâng cao hơn sẽ xử lý trường hợp lỗi
        console.log(error);
      }
    };
    return [storedValue, setValue];
}

export { useLocalStorage }; 