<template>
    <div>
        <div>
            <form @submit.prevent="createClient">
                <div class="form-group">
                    <label for="username">Username:</label><br>
                    <input 
                        type="text"
                        id="username"
                        v-model="formData.userName"
                    />
                    <p v-if="errors.username">{{ errors.username }}</p>
                </div>
                <br>
                <div class="form-group">
                    <label for="email">Email:</label><br>
                    <input 
                        type="text"
                        id="email"
                        v-model="formData.email"
                    />
                    <p v-if="errors.email">{{ errors.email }}</p>
                </div>
                <br>
                <div class="form-action">
                    <input type="submit" value="Save">
                </div>
            </form>
        </div>

        <p v-if="successMessage" class="success-message">{{ successMessage }}</p>
        <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    </div>
</template>

<script>
import apiService from '@/services/api.service';

export default {
	name: "ClientForm",
    data() {
        return {
            formData: {
                userName: '',
                email: ''
            },
            errors: {
                email: '',
                username: ''
            },
            successMessage: '',
            errorMessage: ''
        };
    },
    methods: {
        validateForm() {
            this.errors.username = this.formData.userName ? '' : 'Username is required';
            this.errors.email = this.formData.email ? '' : 'Email is required';

            return !this.errors.username && !this.errors.email;
        },
        async createClient() {
            if (this.validateForm()) {
                this.successMessage = '';
                this.errorMessage = '';
                const url = '/api/client';

                try {
                    await apiService.postFormData(url, this.formData, { "Content-Type": "application/json" });
                    
                    this.successMessage =  `User ${this.formData.userName} created successfully`;
                    this.formData.userName = '';
                    this.formData.email = '';
                } catch (error) {
                    this.errorMessage = `Failed to submit: ${error.message}`;
                }
            }
        }
    }
};
</script>