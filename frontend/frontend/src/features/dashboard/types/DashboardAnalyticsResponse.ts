export interface DashboardAnalyticsResponse {

    generatedAtUtc: string;

    overview: KPIOverview;

    monthlyTrend: MonthlyTrend[];

    projectProgress: ProjectProgress[];

    teamPerformance: TeamPerformance[];

    topPerformers: TopPerformer[];

}

export interface KPIOverview {

    totalProjects: number;

    totalUsers: number;

    totalTasks: number;

    completedTasks: number;

    activeTasks: number;

    overdueTasks: number;

    estimatedHours: number;

    actualHours: number;

    completionPercentage: number;

}

export interface MonthlyTrend {

    year: number;

    month: number;

    tasksCreated: number;

    tasksCompleted: number;

    hoursLogged: number;

}

export interface ProjectProgress {

    projectId: string;

    projectName: string;

    progress: number;

    totalTasks: number;

    completedTasks: number;

    estimatedHours: number;

    actualHours: number;

}

export interface TeamPerformance {

    userId: string;

    userName: string;

    assignedTasks: number;

    completedTasks: number;

    completionRate: number;

    hoursLogged: number;

}

export interface TopPerformer {

    userId: string;

    userName: string;

    productivityScore: number;

}
