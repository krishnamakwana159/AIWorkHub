export interface ProjectReport {
    projectId: string;
    projectName: string;
    totalTasks: number;
    todoTasks: number;
    inProgressTasks: number;
    reviewTasks: number;
    doneTasks: number;
    completionPercentage: number;
    estimatedHours: number;
    actualHours: number;
    overdueTasks: number;
}

export interface UserProductivityReport {
    userId: string;
    userName: string;
    assignedTasks: number;
    completedTasks: number;
    inProgressTasks: number;
    todoTasks: number;
    reviewTasks: number;
    overdueTasks: number;
    estimatedHours: number;
    actualHours: number;
    completionRate: number;
    productivityScore: number;
}
