<template>
  <q-page class="flex flex-center">
    <q-form
      class="q-gutter-lg q-pa-sm"
      style="width: 800px;"
    >
      <h4>Add Receiver</h4>
      <div class="row">
        <q-input
          class="col q-mr-sm"
          v-model="receiverForm.fullname"
          label="Name"
          lazy-rules
        />
        <q-input
          class="col"
          v-model="receiverForm.email"
          label="Email"
          lazy-rules
        />
      </div>
      <q-btn @click="addReceiver" color="primary" icon="mail" label="Add Receiver" />
    </q-form>
    <q-form
      @submit="sendData"
      class="q-gutter-lg q-pa-sm"
      style="width: 800px;"
    >
      <h4>Send Spam</h4>
      <div class="row">
        <q-select
          v-model="form.email"
          :options="emails"
          option-label="email"
          label="Emails"
          class="col q-mr-sm"
        />
        <q-input
          class="col"
          v-model="form.title"
          label="Email title"
          lazy-rules
          :rules="[val => (val && val.length > 0) || 'Please type something']"
        />
      </div>
      <div class="col">
        <q-input
          v-model="form.description"
          label="Email description"
          type="textarea"
          style="max-height: 400px"
        />
      </div>
      <q-btn type="submit" color="primary" icon="mail" label="Send spam" />
    </q-form>
  </q-page>
</template>

<script>
import { api } from "boot/axios";
import moment from 'moment';

export default {
  name: "AddSpam",
  data() {
    return {
      form: {
        email: "",
        title: "",
        description: "",
      },
      receiverForm: {
        fullname: "",
        email: ""
      },
      emails: []
    };
  },
  methods: {
    loadEmails() {
      api
        .get("/api/Receiver",{
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }})
        .then(response => {
          this.emails = response.data;
        })
        .catch(() => {
          this.$q.notify({
            color: "negative",
            position: "top",
            message: "Loading failed",
            icon: "report_problem"
          });
        });
    },
    sendData() {
      const bodyFormData = new FormData();
      bodyFormData.append('ReceiverId', this.form.email.id)
      bodyFormData.append('Title', this.form.title)
      bodyFormData.append('Description', this.form.description)
      bodyFormData.append('SendingDate', moment().add(2, 'h').add(1, 'm').format())
      api
        .post(
          "/api/Email",
          bodyFormData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
              'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        .then(response => {
          this.$q.notify({
            color: "positive",
            position: "top",
            message: "Email will be sent in a minute"
          });
        })
        .catch(() => {
          this.$q.notify({
            color: "negative",
            position: "top",
            message: "Sending failed",
            icon: "report_problem"
          });
        });
    },
    addReceiver() {
      api
        .post("/api/Receiver", this.receiverForm, {
          headers: {
            'Authorization': `bearer ${localStorage.getItem('jwt')}`
          }
        })
        .then(response => {
          this.$q.notify({
            color: "positive",
            position: "top",
            message: "Data sent"
          });
          this.loadEmails();
        })
        .catch(() => {
          this.$q.notify({
            color: "negative",
            position: "top",
            message: "Sending failed",
            icon: "report_problem"
          });
        });
    }
  },
  created() {
    this.loadEmails();
  }
};
</script>
