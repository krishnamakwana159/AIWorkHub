import { z } from "zod";

import { ProjectPriority } from "@/shared/constants/project";

export const projectSchema = z.object({

    name: z
        .string()
        .min(3, "Project name is required"),

    description: z.string().optional(),

    color: z.string(),

    priority: z.union([
        z.literal(ProjectPriority.Low),
        z.literal(ProjectPriority.Medium),
        z.literal(ProjectPriority.High),
        z.literal(ProjectPriority.Critical)
    ]),

    startDateUtc: z.string().optional(),

    targetCompletionDateUtc:
        z.string().optional()

});

export type ProjectFormValues =
    z.infer<typeof projectSchema>;
