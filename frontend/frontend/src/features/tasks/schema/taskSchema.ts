import { z } from "zod";

export const taskSchema = z.object({

    title: z
        .string()
        .min(3, "Task title is required"),

    description:
        z.string().optional(),

    priority:
        z.number(),

    estimatedHours:
        z.number(),

    startDateUtc:
        z.string().optional(),

    dueDateUtc:
        z.string().optional()

});

export type TaskFormValues =
    z.infer<typeof taskSchema>;
