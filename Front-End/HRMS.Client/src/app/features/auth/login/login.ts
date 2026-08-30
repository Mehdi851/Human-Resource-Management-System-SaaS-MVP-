import {
  ChangeDetectionStrategy, Component, signal,} from '@angular/core';
import {FormControl,FormGroup,ReactiveFormsModule,Validators,} from '@angular/forms';
@Component({
  imports: [ReactiveFormsModule],
  standalone: true,
  selector: 'app-login',
  styleUrl: './login.scss',
  templateUrl: './login.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Login {
   readonly isSubmitting = signal(false);
  readonly showPassword = signal(false);

  readonly loginForm = new FormGroup({
    email: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.email],
    }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required],
    }),
    rememberMe: new FormControl(false, {
      nonNullable: true,
    }),
  });

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);

    // Authentication API integration will be implemented in Phase 3.
    // Temporary UI-only loading demonstration.
    setTimeout(() => {
      this.isSubmitting.set(false);
    }, 1200);
  }

  togglePasswordVisibility(): void {
    this.showPassword.update(value => !value);
  }

  get emailControl(): FormControl<string> {
    return this.loginForm.controls.email;
  }

  get passwordControl(): FormControl<string> {
    return this.loginForm.controls.password;
  }
}
