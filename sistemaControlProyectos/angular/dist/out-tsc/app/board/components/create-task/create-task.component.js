import { __decorate } from "tslib";
import { Component, Input, ViewChild } from '@angular/core';
import { Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
let CreateTaskComponent = class CreateTaskComponent {
    constructor(fb, _ngZone, tasksService) {
        this.fb = fb;
        this._ngZone = _ngZone;
        this.tasksService = tasksService;
        this.priorities = [
            { value: 'urgent', viewValue: 'Urgente' },
            { value: 'moderate', viewValue: 'Moderado' },
            { value: 'low', viewValue: 'Bajo' },
        ];
    }
    ngOnInit() {
        this.setForm();
        this.selectedPriority = '';
        if (this.task && this.task.IDActividad && this.task.IDActividad > 0) {
            this.setValuesOnForm(this.task);
            this.formText = 'Editar';
            this.selectedPriority = this.task.proceso;
        }
        else {
            this.formText = 'Crear';
        }
    }
    setForm() {
        this.createTask = this.fb.group({
            date: [new Date(), Validators.required],
            priority: ['urgent', Validators.required],
            description: ['', Validators.required],
        });
    }
    onFormAdd(form) {
        if (this.createTask.valid && this.task && !this.task.IDActividad) {
            this.tasksService.addTask(form);
            this.close();
        }
        else if (this.task && this.listId) {
            const findPriority = this.priorities.find((element) => form.proceso === element.value);
            form.IDActividad = this.task.IDActividad;
            form.proceso = !findPriority ? this.task.proceso : form.proceso;
            form.FechaFin = new Date(form.FechaFin);
            if (form.proceso) {
                this.tasksService.updateTask(form, this.listId);
            }
            this.close();
        }
    }
    triggerResize() {
        // Wait for changes to be applied, then trigger textarea resize.
        this._ngZone.onStable.
            pipe(take(1))
            .subscribe(() => this.autosize.resizeToFitContent(true));
    }
    setValuesOnForm(form) {
        this.createTask.setValue({
            date: new Date(form.FechaFin),
            priority: form.proceso,
            description: form.Descripcion
        });
    }
    close() {
        this.connectedOverlay.overlayRef.detach();
    }
};
__decorate([
    ViewChild('autosize')
], CreateTaskComponent.prototype, "autosize", void 0);
__decorate([
    Input()
], CreateTaskComponent.prototype, "listId", void 0);
__decorate([
    Input()
], CreateTaskComponent.prototype, "task", void 0);
__decorate([
    Input()
], CreateTaskComponent.prototype, "connectedOverlay", void 0);
CreateTaskComponent = __decorate([
    Component({
        selector: 'app-create-task',
        templateUrl: './create-task.component.html',
        styleUrls: ['./create-task.component.scss']
    })
], CreateTaskComponent);
export { CreateTaskComponent };
//# sourceMappingURL=create-task.component.js.map