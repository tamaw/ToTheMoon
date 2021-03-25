<template>
  <div class="container">
    <h1 class="mt-1">Your cryptocurrency of choice</h1>

    <div class="row">
      <div class="form-group row">
        <label class="col-4 col-form-label" for="preferredCoin">Coin:</label>
        <div class="col-8">
          <select
            id="preferredCoin"
            name="preferredCoin"
            required="required"
            class="custom-select"
            v-model="preferredCoin"
            @change="onChange($event)"
          >
            <option v-for="coin in coins" :value="coin" :key="coin">
              {{ coin }}
            </option>
          </select>
        </div>
      </div>
    </div>

    <hr />

    <h1 class="mt-1">{{ preferredCoin }}</h1>
    <div class="row">
      <p>Ask Price: ${{ askPrice }}</p>
    </div>
  </div>
</template>
<script>
export default {
  props:  {

  },
  data() {
    return {
      coins: ["BTC", "ETH", "XRP"],
      preferredCoin: null,
      askPrice: "...",
      previousAskPrice: null,
      precentDifference: null,
    };
  },
  created() {
    this.$http.get("/api/PreferredCoin").then((res) => {
      this.preferredCoin = res.data.preferredCoin;

      this.$http.get("/api/CoinPrice/" + this.preferredCoin).then((res) => {
        this.askPrice = res.data.askPrice;
      });
    });
    console.log("hereiam");
  },
  methods: {
    async onChange(e) {
      this.$http
        .post("/api/PreferredCoin", { coin: e.target.value })
        .then(async (res) => {
          this.preferredCoin = res.data.changedTo;
          this.askPrice = "...";

          await this.$http
            .get("/api/CoinPrice/" + this.preferredCoin)
            .then((res) => {
              this.askPrice = res.data.askPrice;
            });
        });
    },
  },
};
</script>
