import {ChangeDetectionStrategy,Component,signal,} from '@angular/core';
import {FormControl,FormGroup,ReactiveFormsModule,Validators,} from '@angular/forms';
import { RouterLink } from '@angular/router';
@Component({
  imports: [  ReactiveFormsModule,RouterLink,],
  selector: 'app-forgot-password',
  styleUrl: './forgot-password.scss',
  templateUrl: './forgot-password.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ForgotPassword {
   readonly isSubmitting = signal(false);
  readonly isSuccess = signal(false);

  readonly forgotPasswordForm = new FormGroup({
    email: new FormControl('', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.email,
      ],
    }),
  });

  get emailControl(): FormControl<string> {
    return this.forgotPasswordForm.controls.email;
  }

  onSubmit(): void {
    if (this.forgotPasswordForm.invalid) {
      this.forgotPasswordForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.isSuccess.set(false);

    // Temporary UI-only simulation.
    // Real password-reset API integration will be implemented in Phase 3.
    setTimeout(() => {
      this.isSubmitting.set(false);
      this.isSuccess.set(true);
    }, 1200);
  }
}
