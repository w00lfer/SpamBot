<template>
  <div
    class="window-height window-width row justify-center items-center"
    style="background: linear-gradient(#8274C5, #5A4A9F);"
  >
    <div class="column q-pa-lg">
      <div class="row">
        <q-card square class="shadow-24" style="width:300px;height:485px;">
          <q-card-section class="bg-deep-purple-7">
            <h4 class="text-h5 text-white q-my-md">Login</h4>
          </q-card-section>
          <q-card-section>
            <q-form class="q-px-sm q-pt-xl">
              <q-input
                square
                clearable
                v-model="username"
                type="username"
                label="Username"
              >
                <template v-slot:prepend>
                  <q-icon name="person" />
                </template>
              </q-input>
              <q-input
                square
                clearable
                v-model="password"
                type="password"
                label="Password"
              >
                <template v-slot:prepend>
                  <q-icon name="lock" />
                </template>
              </q-input>
            </q-form>
          </q-card-section>
          <q-card-actions class="q-px-lg">
            <q-btn
              @click="handleLogin"
              unelevated
              size="lg"
              color="purple-4"
              class="full-width text-white"
              label="Sign In"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
    <div class="column q-pa-lg">
      <div class="row">
        <q-card square class="shadow-24" style="width:300px;height:485px;">
          <q-card-section class="bg-deep-purple-7">
            <h4 class="text-h5 text-white q-my-md">Registration</h4>
          </q-card-section>
          <q-card-section>
            <q-form class="q-px-sm q-pt-xl q-pb-lg">
              <q-input
                square
                clearable
                v-model="email"
                type="email"
                label="Email"
              >
                <template v-slot:prepend>
                  <q-icon name="email" />
                </template>
              </q-input>
              <q-input
                square
                clearable
                v-model="username"
                type="username"
                label="Username"
              >
                <template v-slot:prepend>
                  <q-icon name="person" />
                </template>
              </q-input>
              <q-input
                square
                clearable
                v-model="password"
                type="password"
                label="Password"
              >
                <template v-slot:prepend>
                  <q-icon name="lock" />
                </template>
              </q-input>
            </q-form>
          </q-card-section>
          <q-card-actions class="q-px-lg">
            <q-btn
              @click="handleRegister"
              unelevated
              size="lg"
              color="purple-4"
              class="full-width text-white"
              label="Register"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script>
import { api } from "boot/axios";

export default {
  name: "Login",
  data() {
    return {
      email: "",
      username: "",
      password: ""
    };
  },
  methods: {
    handleLogin(e) {
      e.preventDefault();
      if (this.password.length > 0) {
        api
          .post("/api/Auth/Login", {
            username: this.username,
            password: this.password
          })
          .then(response => {
            localStorage.setItem("jwt", response.data.token);

            if (localStorage.getItem("jwt") != null) {
              this.$emit("loggedIn");
              this.$router.push('/home');
            }
          })
          .catch(function(error) {
            console.error(error);
          });
      }
    },
    handleRegister(e) {
      e.preventDefault();
      if (this.password.length > 0) {
        api
          .post("/api/Auth/Regiser", {
            email: this.email,
            username: this.username,
            password: this.password
          })
          .then(response => {
            this.handleLogin(e)
          })
          .catch(function(error) {
            console.error(error);
          });
      }
    }
  }
};
</script>

<style></style>
