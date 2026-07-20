import { Stack } from "@mui/material";
import { FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import FormSelect from "@/shared/forms/FormSelect";
import FormTextField from "@/shared/forms/FormTextField";
import { ProjectPriority, ProjectPriorityInfo } from '@/shared/constants/project'

import {
    projectSchema,
    type ProjectFormValues
} from "../schema/projectSchema";

type Props = {
    defaultValues: ProjectFormValues;
    onSubmit(values: ProjectFormValues): void;
};

export default function ProjectForm({
    defaultValues,
    onSubmit
}: Props) {

    const methods =
        useForm<ProjectFormValues>({
            resolver:
                zodResolver(projectSchema),
            defaultValues
        });

    return (

        <FormProvider {...methods}>
            <form id="project-form"
                onSubmit={
                    methods.handleSubmit(onSubmit)
                }
            >

                <Stack spacing={3}>
                    <FormTextField
                        name="name"
                        label="Project Name"
                    />

                    <FormTextField
                        name="description"
                        label="Description"
                        multiline
                        rows={4}
                    />

                    <FormTextField
                        name="color"
                        label="Color"
                    />

                    <FormSelect
                        name="priority"
                        label="Priority"
                        options={Object.values(ProjectPriority)
                            .filter(
                                value =>
                                    typeof value === "number"
                            )
                            .map(priority => ({
                                value: priority,
                                label:
                                    ProjectPriorityInfo[
                                        priority
                                    ].label
                            }))}
                    />
                </Stack>
            </form>
        </FormProvider>

    );

}
