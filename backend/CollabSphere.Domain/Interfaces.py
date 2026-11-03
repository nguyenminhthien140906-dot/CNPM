from abc import ABC, abstractmethod
from typing import List, Optional
# Lấy các classes từ file Entities.py
from .Entities import User, Group, Task, Workspace

class IUserRepository(ABC):
    @abstractmethod
    def get_by_id(self, user_id: str) -> Optional[User]:
        pass

    @abstractmethod
    def save(self, user: User) -> None:
        pass

class ITaskRepository(ABC):
    @abstractmethod
    def get_tasks_by_group(self, group_id: str) -> List[Task]:
        pass

    @abstractmethod
    def save(self, task: Task) -> None:
        pass

class IWorkspaceRepository(ABC):
    @abstractmethod
    def get_by_group(self, group_id: str) -> Optional[Workspace]:
        pass
        
    @abstractmethod
    def save_snapshot(self, workspace: Workspace) -> None:
        pass
