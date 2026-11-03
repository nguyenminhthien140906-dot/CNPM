from dataclasses import dataclass
import re

@dataclass(frozen=True)
class Email:
    value: str

    def __post_init__(self):
        # Kiểm tra định dạng email hợp lệ
        if not re.match(r"[^@]+@[^@]+\.[^@]+", self.value):
            raise ValueError("Địa chỉ email không hợp lệ!")

@dataclass(frozen=True)
class Score:
    value: float

    def __post_init__(self):
        # Điểm số đánh giá chỉ được phép từ 0 đến 10
        if self.value < 0 or self.value > 10:
            raise ValueError("Điểm số phải nằm trong khoảng từ 0 đến 10.")
