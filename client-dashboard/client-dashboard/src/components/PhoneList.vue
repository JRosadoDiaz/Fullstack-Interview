<template>
    <div class="phoneList">
        <p v-if="!phones">There are currently no phones for this user</p>
        <table v-if="phones" class="phoneTable">
            <tr>
                <th>Phone Numbers</th>
            </tr>
            <tr v-for="(item, index) in phones" :key="index">
                <td class="phoneCol">
                    <div v-if="item.editPhone">
                        <input type="text" v-model="item.editedPhone" />
                    </div>
                    <div v-else>
                        <p>{{ item.phone.phoneNumber }}</p>
                    </div>
                    <p v-if="item.phoneMessage">{{ item.phoneMessage }}</p>
                </td>
                <td class="phoneCol">
                    <button v-if="item.editPhone" type="button" @click="savePhone(item)">Save</button>
                    <button type="button" @click="item.editPhone = !item.editPhone">{{ item.editPhone ? 'Cancel' : 'Edit' }}</button>
                    <button v-if="!item.editPhone" type="button" @click="deletePhone(item)">Remove</button>
                </td>
            </tr>
        </table>
        <button type="button" @click="showForm = !showForm">Add New Number</button>
        <div v-show="showForm">
            <br/>
            <form @submit.prevent="createPhone">
                <div class="form-group">
                    <label for="phone">Phone:</label>
                    <input
                        type="text"
                        id="phone"
                        v-model="formData.phoneNumber"
                    />
                    <div class="form-action">
                        <input type="submit" value="Save">
                    </div>
                    <div v-if="phoneError">
                        <div v-for="(error, index) in phoneError" :key="index">
                            <p>{{ error }}</p>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        <p v-if="phoneSuccess">{{ phoneSuccess }}</p>
    </div>
</template>

<script>
import apiService from "@/services/api.service.js";

export default {
    props: {
        clientId: String
    },
    created() {
        this.getPhones(this.clientId);
    },
    data() {
        return {
            phones: [],
            showForm: false,
            formData: {
                clientid: parseInt(this.clientId),
                phoneNumber: null,
            },
            phoneError: [],
            phoneSuccess: null
        }
    },
    methods: {
        async getPhones(id) {
            const url = `/api/phone/client/${id}`;
            try {
                let phoneList = await apiService.get(url);
                phoneList.forEach(phone => {
                    let item = {
                        editPhone: false,
                        editedPhoneData: {phoneNumber: phone.phoneNumber},
                        phoneMessage: null,
                        phone
                    };
                    this.phones.push(item)
                });
            } catch (err) {
                this.error = err;
                this.phones = null;
            }
        },
        validateForm() {
            if (this.formData.phoneNumber == '') {
                this.phoneError.push('Phone is required');
            }
            if (this.formData.phoneNumber.length > 20) {
                this.phoneError.push('Phone cannot exceed 20 characters');
            }

            return this.phoneError.length === 0;
        },
        validatePhone(phoneItem) {
            return phoneItem.phone.phoneNumber != phoneItem.editedPhone;
        },
        async createPhone() {
            if (this.validateForm()) {
                this.phoneSuccess = '';
                const url = `/api/phone`;
                try {
                    await apiService.postFormData(url, this.formData, {"Content-Type": "application/json" });
                    this.phoneSuccess = `Phone has been saved successfully`;
                    this.formData.phoneNumber = '';
                    this.showForm = false;
                } catch (error) {
                    this.errorMessage = `Failed to submit: ${error.message}`;
                }
            }
        },
        async deletePhone(phoneItem) {
            if (confirm(`Delete ${phoneItem.phone.phoneNumber}?`)) {
                const url = `/api/phone/${phoneItem.phone.id}`;
                try {
                    await apiService.del(url);
                    this.phoneSuccess = `Phone number deleted successfully`;
                } catch (error) {
                    this.errorMessage = `Failed to submit: ${error.message}`;
                }
            }
        },
        async savePhone(phoneItem) {
            if (this.validatePhone(phoneItem)) {
                const url =`/api/phone/${phoneItem.phone.id}`;
                let phoneObj = {
                    clientId: parseInt(this.clientId),
                    phoneNumber: phoneItem.editedPhone
                }
                try {
                    await apiService.put(url, phoneObj, { "Content-Type": "application/json" });
                    phoneItem.editPhone = false;
                    phoneItem.phoneMessage = "Phone saved successfully";
                } catch (err) {
                    phoneItem.phoneMessage = err;
                }
            }
        }
    }
}
</script>

<style lang="scss">
.phoneTable {
    margin-top: 10px;
}

.phoneCol {
    text-align: center;
    min-width: 50%;
}

.form-action {
    margin-top: 5px;
}

td {
    p {
        margin-top: 4px;
        margin-bottom: 4px;
        text-align: center;
    }
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}
</style>